namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 为比赛数据提供上下文支持。
    /// </summary>
    public partial class ContestDataContext : DbContext
    {
        /// <summary>
        /// 初始化 ContestDataContext 类的新实例。
        /// </summary>
        public ContestDataContext()
            : base("name=ContestDataContext")
        {
        }

        /// <summary>
        /// 将给定的比赛实体数据添加至数据库中。
        /// </summary>
        /// <param name="entity">要添加的比赛实体。</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddContest(ContestEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Contests.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// 按比赛 ID 查询比赛实体对象。
        /// </summary>
        /// <param name="contestId">要查询的比赛 ID 。</param>
        /// <returns>
        /// 具有给定比赛 ID 值得比赛实体对象。若未在数据库中找到对应的比赛实体对象，返回 null 。
        /// </returns>
        public ContestEntity QueryContestById(int contestId)
        {
            return Contests.Find(contestId);
        }

        /// <summary>
        /// 按标题查询比赛实体对象。
        /// </summary>
        /// <param name="title">要查询的标题。</param>
        /// <returns>一个可查询对象，该对象包含了所有标题为给定值的比赛实体对象。</returns>
        /// <exception cref="ArgumentNullException"/>
        public IQueryable<ContestEntity> QueryContestsByTitle(string title)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            var entities = from item in Contests
                           where item.Title == title
                           select item;
            return entities;
        }

        /// <summary>
        /// 按作者查询比赛实体对象。
        /// </summary>
        /// <param name="creator">要查询的作者的用户名。</param>
        /// <returns>一个可查询对象，该对象包含了所有作者为给定值的比赛实体对象。</returns>
        /// <exception cref="ArgumentNullException"/>
        public IQueryable<ContestEntity> QueryContestsByCreator(string creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            var entities = from item in Contests
                           where item.Creator == creator
                           select item;
            return entities;
        }

        /// <summary>
        /// 查询所有未开始的比赛实体对象。
        /// </summary>
        /// <returns>一个可查询对象，该对象可查询到所有的未开始的比赛实体对象。</returns>
        public IQueryable<ContestEntity> QueryUnstartedContests()
        {
            DateTime now = DateTime.Now;
            var entities = from item in Contests
                           where item.StartTime > now
                           select item;
            return entities;
        }

        /// <summary>
        /// 查询所有正在进行的比赛实体对象。
        /// </summary>
        /// <returns>一个可查询对象，该对象可查询到所有的正在进行的比赛实体对象。</returns>
        public IQueryable<ContestEntity> QueryRunningContests()
        {
            DateTime now = DateTime.Now;
            var entities = from item in Contests
                           where item.StartTime <= now && item.EndTime >= now
                           select item;
            return entities;
        }

        /// <summary>
        /// 查询所有已结束的比赛实体对象。
        /// </summary>
        /// <returns>一个可查询对象，该对象可查询到所有的已结束的比赛实体对象。</returns>
        public IQueryable<ContestEntity> QueryEndedContests()
        {
            DateTime now = DateTime.Now;
            var entities = from item in Contests
                           where item.EndTime < now
                           select item;
            return entities;
        }

        /// <summary>
        /// 从数据库中移除给定的比赛数据实体。
        /// </summary>
        /// <param name="entity">要移除的比赛实体对象。</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveContest(ContestEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Contests.Remove(entity);
        }

        /// <summary>
        /// 获取或设置比赛数据集。
        /// </summary>
        protected virtual DbSet<ContestEntity> Contests { get; set; }
    }
}
