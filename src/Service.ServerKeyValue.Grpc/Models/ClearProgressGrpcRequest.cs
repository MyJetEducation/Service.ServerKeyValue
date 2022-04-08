using System.Runtime.Serialization;

namespace Service.ServerKeyValue.Grpc.Models
{
	[DataContract]
	public class ClearProgressGrpcRequest
	{
		[DataMember(Order = 1)]
		public string UserId { get; set; }

		[DataMember(Order = 1)]
		public bool ClearEducationProgress { get; set; }

		[DataMember(Order = 2)]
		public bool ClearAchievements { get; set; }

		[DataMember(Order = 3)]
		public bool ClearStatuses { get; set; }

		[DataMember(Order = 4)]
		public bool ClearHabits { get; set; }

		[DataMember(Order = 5)]
		public bool ClearSkills { get; set; }

		[DataMember(Order = 6)]
		public bool ClearKnowledge { get; set; }

		[DataMember(Order = 7)]
		public bool ClearUserTime { get; set; }

		[DataMember(Order = 8)]
		public bool ClearRetry { get; set; }
	}
}