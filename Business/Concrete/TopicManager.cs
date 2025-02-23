using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TopicManager : ITopicServices
    {
        ITopicDal _topicDal;

        public TopicManager(ITopicDal topicDal)
        {
            _topicDal = topicDal;
        }

        public IResult Add(Topic entity)
        {
            _topicDal.Add(entity);
            return new SuccessResult(Messages.Topic_Add);
        }

        public IResult Delete(Topic entity)
        {
            _topicDal.Delete(entity);
            return new SuccessResult(Messages.Topic_Deleted);
        }

        public IDataResult<List<Topic>> GetAll()
        {
            return new SuccessDataResult<List<Topic>>(_topicDal.GetAll(), Messages.Topic_List);
        }

        public IDataResult<Topic> GetById(int id)
        {
            return new SuccessDataResult<Topic>(_topicDal.Get(x => x.ID == id), Messages.Topics_List);
        }

        public IResult Update(Topic entity)
        {
            _topicDal.Update(entity);
            return new SuccessResult(Messages.Topic_Update);
        }
    }
}
