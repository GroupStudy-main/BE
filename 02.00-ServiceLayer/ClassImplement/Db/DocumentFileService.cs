using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.ClassImplement.Db;

public class DocumentFileService : IDocumentFileService
{
    private IRepoWrapper repos;

    public DocumentFileService(IRepoWrapper repos)
    {
        this.repos = repos;
    }

    
    public IQueryable<DocumentFile> GetList(string type)
    {
        return repos.DocumentFiles.GetList();
    }

    public Task CreateDocumentFile(DocumentFile documentFile)
    {
        return repos.DocumentFiles.CreateAsync(documentFile);
    }

    public Task UpdateDocumentFile(DocumentFile documentFile)
    {
        return repos.DocumentFiles.UpdateAsync(documentFile);
    }

    public Task<DocumentFile> GetById(int id)
    {
        return repos.DocumentFiles.GetByIdAsync(id);
    }
}