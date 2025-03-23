using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Interfaces.ConverterVO;

namespace RestWithASPNET.Domain.ValueObjects.ConverterVO
{
    public class BookConverter : IBook<BookVO, Book>, IBook<Book, BookVO>
    {
        public Book Book(BookVO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                Launch_Date = origin.Launch_Date,
                Price = origin.Price,
                title = origin.title,
                CreatedAt = origin.createdat,
                UpdatedAt = origin.updatedat,
                DeletedAt = origin.deletedat
            };
        }
        public BookVO Book(Book origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                Launch_Date = origin.Launch_Date,
                Price = origin.Price,
                title = origin.title,
                createdat = origin.CreatedAt,
                updatedat = origin.UpdatedAt,
                deletedat = origin.DeletedAt
            };
        }

        public List<Book> Book(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Book(item)).ToList();
        }


        public List<BookVO> Book(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Book(item)).ToList();
        }
    }
}
