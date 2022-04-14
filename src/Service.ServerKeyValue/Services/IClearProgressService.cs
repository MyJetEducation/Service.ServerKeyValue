using System.Threading.Tasks;

namespace Service.ServerKeyValue.Services
{
	public interface IClearProgressService
	{
		public ValueTask<bool> ClearProgress(string userId, bool progress, bool achievements, bool statuses, bool habits, bool skills, bool knowledge, bool userTime, bool retry);
	}
}