using CoreSolution.Domain;
using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto.MyModel
{
    public class MyHouse
    {
        public Building building;
        public DoorCard doorCard;
        public House house;
        public string OrientationName;
    }
}
