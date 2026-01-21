namespace Repository;
using UCore;
using Logger;

public interface IScheduleRepository
{
    public long Create(ScheduleDto schedule);
    public Schedule Get(long Id);
    public List<Schedule> ReturnList();
    public List<Schedule> ReturnListForDirectionId(long dirId);
    public void Delete(long ID);
    public long Update(ScheduleDto schedule);
}