using DataLayer.DBObject;

namespace ServiceLayer.Interface.Db;

public interface IDocumentFileService
{
    IQueryable<DocumentFile> GetList();
    
    IQueryable<DocumentFile> GetListByMeetingId(int meetingId, bool? approved);

    Task CreateDocumentFile(DocumentFile documentFile);
    
    Task UpdateDocumentFile(DocumentFile documentFile);
    
    Task<DocumentFile> GetById(int id);
    
    Task DeleteDocumentFile(int id);
}