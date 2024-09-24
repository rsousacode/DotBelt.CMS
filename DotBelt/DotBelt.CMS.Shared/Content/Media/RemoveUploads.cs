using DotBelt.QueriesMutations;
using HotChocolate.Authorization;

namespace DotBelt.CMS.Shared.CMS.Media;

[ExtendObjectType<GraphQLMutation>]
public class RemoveUploads
{
    [Authorize]
    public async Task<RemoveUploadsResult> DeleteUploads(List<int> uploadIds, [Service] UploadsController uploadsController) 
    {
        return await uploadsController.DeleteUploadsAsync(uploadIds);
    }
}