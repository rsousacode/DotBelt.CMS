using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.Content.Post;

namespace DotBelt.CMS.Shared.Commands.Seeding;

public static class SeedPages
{
    public static Post GetHelloWorld(int authorId, int tenantId)
    {
        var post = new Post()
        {
            Title = "Hello World",
            Description = "A simple example of how to create a new post",
            RelativeUrl = "hello-world",
            FullUrl = "hello-world",
            Content = "{\"time\": 1726398000085, \"blocks\": [{\"id\": \"kO-TVas8BZ\", \"data\": {\"text\": \"Heading 1\", \"level\": 1}, \"type\": \"header\"}, {\"id\": \"DvOeBNy-Q8\", \"data\": {\"text\": \"Heading 2\", \"level\": 2}, \"type\": \"header\"}, {\"id\": \"FBxDcscaDw\", \"data\": {\"text\": \"Heading 3\", \"level\": 3}, \"type\": \"header\"}, {\"id\": \"fYPwU0HPgY\", \"data\": {\"text\": \"Heading 4\", \"level\": 4}, \"type\": \"header\"}, {\"id\": \"36M9Cmz3jk\", \"data\": {\"text\": \"Heading 5\", \"level\": 5}, \"type\": \"header\"}, {\"id\": \"9XPWbQAyTz\", \"data\": {\"text\": \"Heading 6\", \"level\": 6}, \"type\": \"header\"}, {\"id\": \"yhwdZgjDP3\", \"data\": {\"text\": \"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean enim nibh, consectetur id condimentum euismod, rhoncus a ante. Nullam venenatis tincidunt dictum. Aenean viverra tellus vitae nisi gravida tempus. Maecenas accumsan rhoncus convallis. Integer venenatis dolor eget accumsan mollis. Ut finibus pretium leo sed eleifend. Nunc luctus nibh in massa vestibulum, a egestas sapien mollis.\"}, \"type\": \"paragraph\"}, {\"id\": \"m2PcjlOq6f\", \"data\": {\"text\": \"Nulla facilisi. Duis hendrerit mattis laoreet. Etiam eu purus dolor. Integer nunc velit, ultrices et auctor vel, ultrices quis felis. Sed tempor, felis eget tempor ornare, nunc nisl dignissim eros, consequat imperdiet nunc leo elementum nisl. Mauris lobortis elementum posuere. Maecenas sed ornare lacus. Morbi ac interdum justo. Quisque tempus nibh ex.\"}, \"type\": \"paragraph\"}, {\"id\": \"lGOoZV63Rv\", \"data\": {\"text\": \"Nunc nec ultrices odio, vel rutrum tellus. Nulla pharetra nulla eget nisl varius, nec commodo nibh aliquet. Etiam faucibus mauris odio, sit amet bibendum eros placerat a. Mauris pretium mauris eget lacus rutrum, maximus euismod urna bibendum. Nulla fringilla aliquam mi, ut varius est. Curabitur ac mi eget purus elementum malesuada sit amet id justo. Phasellus eget interdum elit, quis pretium velit. Aenean facilisis quam purus, eu semper lacus suscipit ac. In sollicitudin, eros id volutpat molestie, massa elit volutpat risus, a egestas nulla urna in quam. Donec eget massa vestibulum, tempor velit rutrum, tristique est. In tempus mauris eu arcu luctus pulvinar.\"}, \"type\": \"paragraph\"}], \"version\": \"2.30.6\"}",
            PublishDate = new DateTime(2024, 9, 15).ToUniversalTime(),
            PostType = PostTypeEnum.Post,
            Status = PostStatus.Draft,
            Author = null!,
            AuthorId = authorId,
            Tenant = null!,
            TenantId = tenantId,
        };
        
        return post;
    }
}