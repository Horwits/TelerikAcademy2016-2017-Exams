using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Contracts;

namespace Dealership.Models
{
    // This class is a halper and has to be in the common folder, but by condition we can't add classes to other folders.
    public class CommentPrinter
    {
        public static string PrintComments(ICollection<IComment> comments)
        {
            var builder = new StringBuilder();

            if (comments.Count == 0)
            {
                builder.AppendLine(string.Format("{0}--NO COMMENTS--", new string(' ', 4)));
            }
            else
            {
                builder.AppendLine(string.Format("{0}--COMMENTS--", new string(' ', 4)));
                var counter = 1;
                foreach (var comment in comments)
                {
                    builder.AppendLine(string.Format("{0}", comment.ToString()));
                    counter++;
                }

                builder.AppendLine(string.Format("{0}--COMMENTS--", new string(' ', 4)));
            }

            return builder.ToString().TrimEnd();
        }
    }
}
