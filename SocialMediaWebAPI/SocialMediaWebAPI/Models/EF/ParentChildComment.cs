using System;
using System.Collections.Generic;

namespace SocialMediaWebAPI.Models.EF
{
    public partial class ParentChildComment
    {
        public int? ParentCommentId { get; set; }
        public int? ChildCommentId { get; set; }

        public virtual Comment? ChildComment { get; set; }
        public virtual Comment? ParentComment { get; set; }
    }
}
