using System.Collections.Generic;

namespace WebAppAlexey.BLL.ViewModels
{
    public class ResultViewModel
    {
        public ResultViewModel(int flag, string information, IEnumerable<object> dataSet = null)
        {
            Flag = flag;
            Information = information;
            DataSet = dataSet;
        }

        public int Flag { get; }
        public string Information { get; }
        public IEnumerable<object> DataSet { get; }
    }
}
