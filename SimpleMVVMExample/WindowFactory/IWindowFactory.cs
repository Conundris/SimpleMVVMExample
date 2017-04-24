using System.Threading.Tasks;

namespace SimpleMVVMExample.WindowFactory
{
    public interface IWindowFactory
    {
        Task<bool?> CreateNewWindow(object selectedItem);
    }
}
