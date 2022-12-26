using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class rangeYearAttribute:RangeAttribute
	{
        public rangeYearAttribute(int minimum): base( minimum, DateTime.Now.Year+1) { }
    }
}
