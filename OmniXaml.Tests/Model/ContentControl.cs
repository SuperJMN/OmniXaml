using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniXaml.Attributes;

namespace OmniXaml.Tests.Model
{
    public class ContentControl : ModelObject
    {
        private ModelObject content;

        [Content]
        public ModelObject Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                ContentAssociatedBeforeAssignment = content.Name == null;
            }
        }

        public bool ContentAssociatedBeforeAssignment { get; private set; }
    }
}
