using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.ValueObjects;
using RestWithASPNET.FrameWrkDrivers.Gateways;

namespace RestWithASPNET.FrameWrkDrivers.ConverterVO
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
                title = origin.title
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
                title = origin.title
            };
        }

        public List<Book> Book(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public List<BookVO> Book(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
