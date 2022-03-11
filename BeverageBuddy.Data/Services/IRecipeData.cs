using BeverageBuddy.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageBuddy.Data.Services
{
    public interface IRecipeData
    {
        IEnumerable<Recipe> GetAll();
    }
}
