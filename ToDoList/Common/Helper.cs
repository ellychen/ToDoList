namespace ToDoList.Common
{
    public static class StringExteison
    {
        public static DateTime? ToDateTime(this string val , DateTime? DefaultValue = null )
        {
            DateTime Result = DateTime.Today; 
            if (DateTime.TryParse(val , out Result))
            {
                return Result;
            }
            else
            {
                return DefaultValue;
            }
        }

        public static int? ToInt(this string val , int? DefaultValue =null )
        {
            int Result = 0; 
            if (int.TryParse(val , out Result))
            {
                return Result;
            }
            else
            {
                return DefaultValue;
            }
        }

        public static decimal? ToDecimal(this string val , decimal? DefaultValue = null )
        {
            decimal Result = 0; 
            if (decimal.TryParse(val , out Result))
            {
                return Result;
            }
            else
            {
                return DefaultValue;
            }
        }




    }



    public class Helper
    {
        


    }
}
