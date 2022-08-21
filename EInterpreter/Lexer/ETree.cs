using System.Collections.Generic;
using System.Linq;
using System.Text;
using EInterpreter.EElements;

namespace EInterpreter.EObjects
{
    public class ETree
    {
        public List<EConstant> Constants { get; set; } = new List<EConstant>();
        public List<EObject> Objects { get; set; } = new List<EObject>();
        public List<EUtility> Utilities { get; set; } = new List<EUtility>();

        public List<EFunction> Functions => Utilities.SelectMany(u => u.Functions).ToList();

        public string Summarize()
        {
            var result = new StringBuilder();

            // constants
            result.AppendLine($"Constants: {Constants.Count}");

            // objects
            result.AppendLine($"Objects: {Objects.Count}");

            // utilities
            result.AppendLine($"Utilities: {Utilities.Count}");

            return result.ToString();
        }
    }
}
