using System;
using System.Collections.Generic;
using Entities.Models.DataTransferObjects;

namespace Contracts
{
    public interface ICartService
    {
        IEnumerable<CartDTO> GetAll();
        void CreateCart(CartViewModel model);
        void UpdateCart(CartViewModel model);
    }
}
