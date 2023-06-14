using DataLayer.DBObject;

namespace ServiceLayer.Interface.Db;

public interface IDocumentFileService
{
    IQueryable<DocumentFile> GetList(string type);

    Task CreateDocumentFile(DocumentFile documentFile);
    
    Task UpdateDocumentFile(DocumentFile documentFile);
    
    Task<DocumentFile> GetById(int id);
}