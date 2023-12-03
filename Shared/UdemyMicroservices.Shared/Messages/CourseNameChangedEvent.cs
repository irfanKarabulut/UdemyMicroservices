using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UdemyMicroservices.Shared.Messages
{
    public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }
        public string UpdatedName { get; set; }
    }
}
