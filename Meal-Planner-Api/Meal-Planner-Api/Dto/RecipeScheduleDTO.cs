﻿namespace Meal_Planner_Api.Dto
{
    public class RecipeScheduleDTO
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int? RecipeId { get; set; }
        public int UserId { get; set; } 
    }
}
