using AutoMapper;
using Bookss.Models.Entities;
using Books.Models.ViewModels.Authors;
using Books.Services.Interfaces;
using Books.Business.Repositories; 

namespace Books.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IRepository<Author> authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync()
        {
            
            var authors = await _authorRepository.GetAllAsync(a => a.Books);
            return _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
        }

        public async Task<AuthorViewModel?> GetAuthorByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id, a => a.Books);
            return _mapper.Map<AuthorViewModel>(author);
        }

        public async Task<AuthorCreateOrEditViewModel?> GetAuthorForEditAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return null;

            return _mapper.Map<AuthorCreateOrEditViewModel>(author);
        }

        public async Task CreateAuthorAsync(AuthorCreateOrEditViewModel model)
        {
            var author = _mapper.Map<Author>(model);

           
            if (author.Id == Guid.Empty)
            {
                author.Id = Guid.NewGuid();
            }

            await _authorRepository.AddAsync(author);
            await _authorRepository.CommitAsync();
        }

        public async Task UpdateAuthorAsync(AuthorCreateOrEditViewModel model)
        {
            if (model.Id == null) return;

            var author = await _authorRepository.GetByIdAsync(model.Id.Value);
            if (author != null)
            {
               

                _authorRepository.Update(author);
                await _authorRepository.CommitAsync();
            }
        }

        public async Task<bool> DeleteAuthorAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return false;

            _authorRepository.Remove(author);
            await _authorRepository.CommitAsync();
            return true;
        }
    }
}