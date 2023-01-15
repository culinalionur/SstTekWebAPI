using MediatR;
using SstTekWebAPI.Linq;

namespace SstTekWebAPI.StudentMediator.GetStudent
{
    public class GetStudentQuery : IRequest<Student>
    {
        public int Number { get; set; }
    }
}
