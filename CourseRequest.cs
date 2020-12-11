using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;


namespace INE.Tasks.SBQueue
{
    public class CourseRequest
    {

        public static List<CourseRequest> GetSampleRequests()
        {
            List<CourseRequest> samples = new List<CourseRequest>();
            samples.AddRange(new List<CourseRequest>{
                new CourseRequest()
                {
                    Title="AZ 204 prep",
                    RequestedBy="Amy",
                    Description="Client wants to see an overview of the AZ-204 exam",
                    Status=CourseRequestStatusEnum.Production
                },
                new CourseRequest()
                {
                    Title="How to be a black hat",
                    RequestedBy="A bad person",
                    Description="Client wants to head down a dark path",
                    Status=CourseRequestStatusEnum.Rejected
                },
                new CourseRequest()
                {
                    Title="Cloud Concepts",
                    RequestedBy="Michelle",
                    Description="Client organization wants an introduction to the cloud for the system administrators",
                    Status=CourseRequestStatusEnum.Approved
                },
                });

            return samples;
        }

        public CourseRequest()
        {
            ID = Guid.NewGuid().ToString();
            RequestedOn = DateTime.UtcNow;
        }

        public string ID { get; set; }
        public string Title { get; set; }
        public DateTime RequestedOn { get; set; }
        public string RequestedBy { get; set; }
        public string Description { get; set; }
        public CourseRequestStatusEnum Status { get; set; }

        public override string ToString()
        {
            return $"{this.RequestedOn:yyyy-MM-dd}\t{this.RequestedBy,-15}\t{this.Title}";
        }

        public byte[] Serialize()
        {
            var serializedString = JsonConvert.SerializeObject(this);
            return Encoding.UTF8.GetBytes(serializedString);
        }

        public static CourseRequest Deserialize(byte[] message)
        {
            var serializedString = Encoding.UTF8.GetString(message);
            return JsonConvert.DeserializeObject<CourseRequest>(serializedString);
        }


    }

    public enum CourseRequestStatusEnum
    {
        Requested,
        Approved,
        Production,
        InReview,
        Rejected
    }
}