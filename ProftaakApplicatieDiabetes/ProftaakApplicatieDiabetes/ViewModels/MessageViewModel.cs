using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;

namespace ProftaakApplicatieDiabetes.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }

        public int CoupleId { get; set; }

        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();

        public bool WasSendMessageSuccess { get; set; }

        public MessageViewModel()
        {
            WasSendMessageSuccess = true;
        }
    }
}
