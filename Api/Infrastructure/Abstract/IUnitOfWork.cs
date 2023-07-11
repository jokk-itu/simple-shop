using System;
using System.Threading.Tasks;

namespace Api.Infrastructure.Abstract;

public interface IUnitOfWork
{
  Task Commit();
}