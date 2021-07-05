using System;

namespace Domain.Entities
{
    public class InstructionItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int StepNumber { get; set; }
        public string Instruction { get; set; }

        
    }
}
