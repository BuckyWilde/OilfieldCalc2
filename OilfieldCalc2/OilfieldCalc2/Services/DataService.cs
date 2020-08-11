using OilfieldCalc2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System.Linq;
using OilfieldCalc2.Models.DrillstringTubulars;
using OilfieldCalc2.Models.WellboreTubulars;
using SQLiteNetExtensions.Extensions;

namespace OilfieldCalc2.Services
{
    /// <summary>
    /// Concrete data service. Responsible for handling CRUD operations.
    /// Inherits from IDataService and is injected wherever required.
    /// </summary>
    public class DataService : IDataService
    {
        static readonly Lazy<SQLiteConnection> lazyInitializer = new Lazy<SQLiteConnection>(() =>
        {
            return new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        });
        static SQLiteConnection database => lazyInitializer.Value;
        private static bool initialized = false;

        /// <summary>
        /// Class constructor. Calls methods to initialize the database tables.
        /// </summary>
        public DataService()
        {
            Initialize<DrillstringTubularBase, WellboreTubularBase>();
        }

        /// <summary>
        /// Initializes the database and creates the tables if they do not already exist
        /// </summary>
        /// <typeparam name="T">Generic representing a database table</typeparam>
        /// <returns></returns>
        private void Initialize<T, TT>()
        {
            if (!initialized)
            {
                if (!database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
                {
                    database.CreateTables(CreateFlags.None, typeof(T));
                    initialized = true;
                }

                if (!database.TableMappings.Any(m => m.MappedType.Name == typeof(TT).Name))
                {
                    database.CreateTables(CreateFlags.None, typeof(TT));
                    initialized = true;
                }
            }
        }
        
        /// <summary>
        /// Deletes a record in the database. Table is choosen based of tubular type
        /// </summary>
        /// <param name="tubular">Tubular item, either wellbore or drillstring</param>
        /// <returns>The number of rows being deleted. This will only delete one record at a time</returns>
        public int DeleteItem(ITubular tubular)
        {
            return database.Delete(tubular);
        }

        /// <summary>
        /// Gets all the tubular items in a given database table. Check the sort order column to ensure
        /// the values are consecutive and corrects them if needed.
        /// </summary>
        /// <typeparam name="T">Generic representing a table in the database</typeparam>
        /// <returns>IEnumerable list of table items</returns>
        public IEnumerable<T> GetTubularItems<T>() where T : ITubular, new()
        {
            bool areConsecutive = true;
            int count = 0;
            List<T> tubularList = database.GetAllWithChildren<T>(recursive: true);
            tubularList.OrderBy(x => x.ItemSortOrder);
            
            //Check to see if ItemSortOrder values are consecutive. Calls method to repair them if not.
            for(count=0;count<tubularList.Count-1;count++)
            {
                if (tubularList[count].ItemSortOrder + 1 != tubularList[count + 1].ItemSortOrder)
                    areConsecutive = false;
            }
            if (!areConsecutive)
            {
                RepairSort<T>();
                tubularList = database.GetAllWithChildren<T>(recursive: true);
            }

            return tubularList.OrderBy(x => x.ItemSortOrder);
        }

        /// <summary>
        /// Saves a tubular item if itemId=0, otherwise it updates an existing record
        /// </summary>
        /// <param name="tubular">Either a wellbore or drillstring tubular object</param>
        /// <returns>1 if sucessful, 0 if no record was saves or updated.</returns>
        public int SaveItem(ITubular tubular)
        {
            if (tubular != null)
            {
                if(tubular.ItemId == 0)
                {
                    //Insert the sortorder numbers here...
                    //Calculate how many records are currently in the database
                    //and make the sort order number one larger!
                    if (tubular is DrillstringTubularBase)
                        tubular.ItemSortOrder = database.Table<DrillstringTubularBase>().Count() + 1;
                    else if (tubular is WellboreTubularBase)
                        tubular.ItemSortOrder = database.Table<WellboreTubularBase>().Count() + 1;
                    else
                        throw new System.ArgumentException("Item being saved is neither a drillstring or wellbore tubular", nameof(tubular));

                    //Insert a new record
                    database.InsertWithChildren(tubular, recursive: true);
                    return 1;
                }

                else
                {
                    //Update an existing record
                    database.UpdateWithChildren(tubular);
                    return 1;
                }
            }
            return 0;
        }

        public ITubular GetItem(int tubularId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears all values from the table
        /// </summary>
        /// <typeparam name="T">Generic parameter representing a database table</typeparam>
        /// <returns>The number is records deleted</returns>
        public int ClearTable<T>()
        {
            return database.DeleteAll<T>();
        }

        /// <summary>
        /// Don't use this... It doesn't work worth a FUCK!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool RepairTable<T>() where T : ITubular, new()
        {
            bool worked = false;

            //Get a list of data and delete or repair the corrupted values
            //var badRecords = await database.Table<DrillstringTubularBase>().Where(x => x.LengthBlobbed = (DBNull)).ToListAsync().ConfigureAwait(false);
            List<T> badRecords = database.Query<T>
                ("SELECT ItemId FROM Drillstring WHERE " +
                "LengthBlobbed is NULL or " +
                "IDBlobbed is NULL or " +
                "ODBlobbed is NULL or " +
                "WeightBlobbed is NULL or " +
                "TubularType is NULL or " +
                "ItemDescription is NULL or " +
                "ItemSortOrder is NULL");
            
            foreach(T badr in badRecords)
            {
                database.Delete<T>(badr.ItemId);
                if (!worked)
                    worked = true;
            }

            RepairSort<T>();

            return worked;
        }

        /// <summary>
        /// Gets a list of items in a table and reassigns sortOrder values
        /// in a consecutive manner
        /// </summary>
        /// <typeparam name="T">Generic representing a database table</typeparam>
        /// <returns>true when complete</returns>
        private bool RepairSort<T>() where T : ITubular, new()
        {
            List<T> tubularList = database.GetAllWithChildren<T>(recursive: true);
            tubularList.OrderBy(x => x.ItemSortOrder);
            int count = 1;
            foreach (T tubular in tubularList)
            {
                tubular.ItemSortOrder = count++;
                SaveItem(tubular);
            }
            return true;
        }
    }
}
