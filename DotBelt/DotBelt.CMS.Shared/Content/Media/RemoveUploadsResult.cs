namespace DotBelt.CMS.Shared.CMS.Media;

public class RemoveUploadsResult
{
    public List<int>? DeletedIds { get; set; }

    public RemoveUploadsResult (List<int>? deletedIds)
    {
        DeletedIds = deletedIds;
    }
}