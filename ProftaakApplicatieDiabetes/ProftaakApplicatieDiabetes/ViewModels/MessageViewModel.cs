using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Models;

namespace ProftaakApplicatieDiabetes.ViewModels
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public string Title { get; set; }

        public List<MessageModel> Messages { get; set; }
    }
}
