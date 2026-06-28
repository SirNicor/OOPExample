using System.Data;
using Dapper;

namespace Repository;

public class TimeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
    }

    public override DateOnly Parse(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return default(DateOnly);
        }

        if (value is DateTime dt)
        {
            return new DateOnly(dt.Year, dt.Month, dt.Day);
        }
        
        if (value is DateOnly d)
            return d;
        
        throw new InvalidCastException($"Cannot convert {value.GetType()} to DateOnly");
    }
}