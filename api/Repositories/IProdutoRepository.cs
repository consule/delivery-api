using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface IProdutoRepository
    {
        Task<List<ProdutoModel>> GetAll();
    }
}
