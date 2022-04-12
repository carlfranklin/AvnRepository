using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
#nullable disable
namespace AvnRepository;
/// <summary>
/// A serializable filter. An alternative to trying to serialize and deserialize LINQ expressions,
/// which are very finicky. This class uses standard types. 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class QueryFilter<TEntity> where TEntity : class
{
    /// <summary>
    /// If you want to return a subset of the properties, you can specify only
    /// the properties that you want to retrieve in the SELECT clause.
    /// Leave empty to return all columns
    /// </summary>
    public List<string> IncludePropertyNames { get; set; } = new List<string>();

    /// <summary>
    /// Defines the property names and values in the WHERE clause
    /// </summary>
    public List<FilterProperty> FilterProperties { get; set; } = new List<FilterProperty>();

    /// <summary>
    /// Specify the property to ORDER BY, if any 
    /// </summary>
    public string OrderByPropertyName { get; set; } = "";

    /// <summary>
    /// Set to true if you want to order DESCENDING
    /// </summary>
    public bool OrderByDescending { get; set; } = false;

    /// <summary>
    /// A custome query that returns a list of entities with the current filter settings.
    /// </summary>
    /// <param name="AllItems"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetFilteredListAsync(List<TEntity> AllItems)
    {
        await Task.Delay(0);

        // Convert to IQueryable
        var query = AllItems.AsQueryable<TEntity>();

        // the expression will be used for each FilterProperty
        Expression<Func<TEntity, bool>> expression = null;

        // Process each property
        foreach (var filterProperty in FilterProperties)
        {
            // use reflection to get the property info
            PropertyInfo prop = typeof(TEntity).GetProperty(filterProperty.Name);

            // string
            if (prop.PropertyType == typeof(string))
            {
                if (filterProperty.Operator == FilterOperator.Equals)
                    if (filterProperty.CaseSensitive == false)
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().ToLower() == filterProperty.Value.ToString().ToLower();
                    else
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString() == filterProperty.Value.ToString();
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    if (filterProperty.CaseSensitive == false)
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().ToLower() != filterProperty.Value.ToString().ToLower();
                    else
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString() != filterProperty.Value.ToString();
                else if (filterProperty.Operator == FilterOperator.StartsWith)
                    if (filterProperty.CaseSensitive == false)
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().ToLower().StartsWith(filterProperty.Value.ToString().ToLower());
                    else
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().StartsWith(filterProperty.Value.ToString());
                else if (filterProperty.Operator == FilterOperator.EndsWith)
                    if (filterProperty.CaseSensitive == false)
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().ToLower().EndsWith(filterProperty.Value.ToString().ToLower());
                    else
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().EndsWith(filterProperty.Value.ToString());
                else if (filterProperty.Operator == FilterOperator.Contains)
                    if (filterProperty.CaseSensitive == false)
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().ToLower().Contains(filterProperty.Value.ToString().ToLower());
                    else
                        expression = s => s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString().Contains(filterProperty.Value.ToString());
            }
            // Int16
            else if (prop.PropertyType == typeof(Int16))
            {
                int value = Convert.ToInt16(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) >= value;
            }
            // Int32
            else if (prop.PropertyType == typeof(Int32))
            {
                int value = Convert.ToInt32(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) >= value;
            }
            // Int64
            else if (prop.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) >= value;
            }
            // UInt16
            else if (prop.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToUInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToUInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToUInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToUInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToUInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToUInt16(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) >= value;
            }
            // UInt32
            else if (prop.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToUInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToUInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToUInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToUInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToUInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToUInt32(s.GetType().GetProperty(filterProperty.Name).GetValue(s)) >= value;
            }
            // UInt64
            else if (prop.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToUInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToUInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToUInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToUInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToUInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToUInt64(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) >= value;
            }
            // DateTime
            else if (prop.PropertyType == typeof(DateTime))
            {
                DateTime value = DateTime.Parse(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => DateTime.Parse(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => DateTime.Parse(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => DateTime.Parse(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => DateTime.Parse(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => DateTime.Parse(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => DateTime.Parse(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) >= value;
            }
            // decimal
            else if (prop.PropertyType == typeof(decimal))
            {
                decimal value =Convert.ToDecimal(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToDecimal(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToDecimal(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToDecimal(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToDecimal(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToDecimal(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToDecimal(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) >= value;
            }
            // Single
            else if (prop.PropertyType == typeof(Single))
            {
                Single value = Convert.ToSingle(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToSingle(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToSingle(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToSingle(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToSingle(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToSingle(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToSingle(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) >= value;
            }
            // Boolean
            else if (prop.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToBoolean(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToBoolean(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
            }
            // Byte
            else if (prop.PropertyType == typeof(Byte))
            {
                Byte value = Convert.ToByte(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToByte(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToByte(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToByte(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToByte(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToByte(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToByte(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) >= value;
            }
            // Char
            else if (prop.PropertyType == typeof(Char))
            {
                Char value = Convert.ToChar(filterProperty.Value);

                if (filterProperty.Operator == FilterOperator.Equals)
                    expression = s => Convert.ToChar(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) == value;
                else if (filterProperty.Operator == FilterOperator.NotEquals)
                    expression = s => Convert.ToChar(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) != value;
                else if (filterProperty.Operator == FilterOperator.LessThan)
                    expression = s => Convert.ToChar(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) < value;
                else if (filterProperty.Operator == FilterOperator.GreaterThan)
                    expression = s => Convert.ToChar(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) > value;
                else if (filterProperty.Operator == FilterOperator.LessThanOrEqual)
                    expression = s => Convert.ToChar(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) <= value;
                else if (filterProperty.Operator == FilterOperator.GreaterThanOrEqual)
                    expression = s => Convert.ToChar(s.GetType().GetProperty(filterProperty.Name).GetValue(s).ToString()) >= value;
            }
            // Add expression creation code for other data types here.

            // apply the expression
            query = query.Where(expression);

        }

        // Include the specified properties
        foreach (var includeProperty in IncludePropertyNames)
        {
            query = query.Include(includeProperty);
        }

        // order by
        if (OrderByPropertyName != "")
        {
            PropertyInfo prop = typeof(TEntity).GetProperty(OrderByPropertyName);
            if (prop != null)
            {
                if (OrderByDescending)
                    query = query.Where(expression).OrderByDescending(x => prop.GetValue(x, null));
                else
                    query = query.Where(expression).OrderBy(x => prop.GetValue(x, null));
            }
        }

        // execute and return the list
        return query.ToList();
    }

}