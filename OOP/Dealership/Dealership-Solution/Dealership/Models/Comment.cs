using System;
using System.Text;
using Dealership.Common;
using Dealership.Contracts;

namespace Dealership.Models
{
    public class Comment : IComment
    {
        private string content;

        public Comment(string content)
        {
            this.Content = content;
        }

        public Comment(string content, string author)
            : this(content)
        {
            this.Author = author;
        }

        public string Content
        {
            get { return this.content; }

            private set
            {
                Validator.ValidateIntRange(value.Length, Constants.MinCommentLength, Constants.MaxCommentLength, string.Format(Constants.StringMustBeBetweenMinAndMax, "Content", Constants.MinCommentLength, Constants.MaxCommentLength));
                this.content = value;
            }
        }

        public string Author { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Format("{0}----------", new string(' ', 4)));
            builder.AppendLine(string.Format("{0}{1}", new string(' ', 4), this.Content));
            builder.AppendLine(string.Format("{0}User: {1}", new string(' ', 6), this.Author));
            builder.AppendLine(string.Format("{0}----------", new string(' ', 4)));

            return builder.ToString().TrimEnd();
        }
    }
}