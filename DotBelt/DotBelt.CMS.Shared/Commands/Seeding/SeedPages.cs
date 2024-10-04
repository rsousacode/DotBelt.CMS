using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.Content.Post;

namespace DotBelt.CMS.Shared.Commands.Seeding;

public static class SeedPages
{
    public static List<Post> GetContent(int authorId, int tenantId)
    {
        var post = new Post()
        {
            Title = "Hello World",
            Description = "A simple example of how to create a new post",
            RelativeUrl = "hello-world",
            FullUrl = "https://boilerplate.com/hello-world",
            Content =
                "{\"time\": 1726398000085, \"blocks\": [{\"id\": \"kO-TVas8BZ\", \"data\": {\"text\": \"Heading 1\", \"level\": 1}, \"type\": \"header\"}, {\"id\": \"DvOeBNy-Q8\", \"data\": {\"text\": \"Heading 2\", \"level\": 2}, \"type\": \"header\"}, {\"id\": \"FBxDcscaDw\", \"data\": {\"text\": \"Heading 3\", \"level\": 3}, \"type\": \"header\"}, {\"id\": \"fYPwU0HPgY\", \"data\": {\"text\": \"Heading 4\", \"level\": 4}, \"type\": \"header\"}, {\"id\": \"36M9Cmz3jk\", \"data\": {\"text\": \"Heading 5\", \"level\": 5}, \"type\": \"header\"}, {\"id\": \"9XPWbQAyTz\", \"data\": {\"text\": \"Heading 6\", \"level\": 6}, \"type\": \"header\"}, {\"id\": \"yhwdZgjDP3\", \"data\": {\"text\": \"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean enim nibh, consectetur id condimentum euismod, rhoncus a ante. Nullam venenatis tincidunt dictum. Aenean viverra tellus vitae nisi gravida tempus. Maecenas accumsan rhoncus convallis. Integer venenatis dolor eget accumsan mollis. Ut finibus pretium leo sed eleifend. Nunc luctus nibh in massa vestibulum, a egestas sapien mollis.\"}, \"type\": \"paragraph\"}, {\"id\": \"m2PcjlOq6f\", \"data\": {\"text\": \"Nulla facilisi. Duis hendrerit mattis laoreet. Etiam eu purus dolor. Integer nunc velit, ultrices et auctor vel, ultrices quis felis. Sed tempor, felis eget tempor ornare, nunc nisl dignissim eros, consequat imperdiet nunc leo elementum nisl. Mauris lobortis elementum posuere. Maecenas sed ornare lacus. Morbi ac interdum justo. Quisque tempus nibh ex.\"}, \"type\": \"paragraph\"}, {\"id\": \"lGOoZV63Rv\", \"data\": {\"text\": \"Nunc nec ultrices odio, vel rutrum tellus. Nulla pharetra nulla eget nisl varius, nec commodo nibh aliquet. Etiam faucibus mauris odio, sit amet bibendum eros placerat a. Mauris pretium mauris eget lacus rutrum, maximus euismod urna bibendum. Nulla fringilla aliquam mi, ut varius est. Curabitur ac mi eget purus elementum malesuada sit amet id justo. Phasellus eget interdum elit, quis pretium velit. Aenean facilisis quam purus, eu semper lacus suscipit ac. In sollicitudin, eros id volutpat molestie, massa elit volutpat risus, a egestas nulla urna in quam. Donec eget massa vestibulum, tempor velit rutrum, tristique est. In tempus mauris eu arcu luctus pulvinar.\"}, \"type\": \"paragraph\"}], \"version\": \"2.30.6\"}",
            PublishDate = new DateTime(2024, 9, 15).ToUniversalTime(),
            PostType = PostTypeEnum.Post,
            Status = PostStatus.Draft,
            Author = null!,
            AuthorId = authorId,
            Tenant = null!,
            TenantId = tenantId,
        };

        var page = new Post()
        {
            Title = "Hello Page",
            Description = "A simple example of how to create a new page",
            RelativeUrl = "hello-page",
            FullUrl = "https://boilerplate.com/hello-page",
            Content =
                "{\n  \"time\": 1728055621440,\n  \"blocks\": [\n    {\n      \"id\": \"kO-TVas8BZ\",\n      \"data\": {\n        \"text\": \"Heading 1\",\n        \"level\": 1\n      },\n      \"type\": \"header\"\n    },\n    {\n      \"id\": \"DvOeBNy-Q8\",\n      \"data\": {\n        \"text\": \"Heading 2\",\n        \"level\": 2\n      },\n      \"type\": \"header\"\n    },\n    {\n      \"id\": \"FBxDcscaDw\",\n      \"data\": {\n        \"text\": \"Heading 3\",\n        \"level\": 3\n      },\n      \"type\": \"header\"\n    },\n    {\n      \"id\": \"fYPwU0HPgY\",\n      \"data\": {\n        \"text\": \"Heading 4\",\n        \"level\": 4\n      },\n      \"type\": \"header\"\n    },\n    {\n      \"id\": \"36M9Cmz3jk\",\n      \"data\": {\n        \"text\": \"Heading 5\",\n        \"level\": 5\n      },\n      \"type\": \"header\"\n    },\n    {\n      \"id\": \"9XPWbQAyTz\",\n      \"data\": {\n        \"text\": \"Heading 6\",\n        \"level\": 6\n      },\n      \"type\": \"header\"\n    },\n    {\n      \"id\": \"yhwdZgjDP3\",\n      \"data\": {\n        \"text\": \"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean enim nibh, consectetur id condimentum euismod, rhoncus a ante. Nullam venenatis tincidunt dictum. Aenean viverra tellus vitae nisi gravida tempus. Maecenas accumsan rhoncus convallis. Integer venenatis dolor eget accumsan mollis. Ut finibus pretium leo sed eleifend. Nunc luctus nibh in massa vestibulum, a egestas sapien mollis.\"\n      },\n      \"type\": \"paragraph\"\n    },\n    {\n      \"id\": \"m2PcjlOq6f\",\n      \"data\": {\n        \"text\": \"Nulla facilisi. Duis hendrerit mattis laoreet. Etiam eu purus dolor. Integer nunc velit, ultrices et auctor vel, ultrices quis felis. Sed tempor, felis eget tempor ornare, nunc nisl dignissim eros, consequat imperdiet nunc leo elementum nisl. Mauris lobortis elementum posuere. Maecenas sed ornare lacus. Morbi ac interdum justo. Quisque tempus nibh ex.\"\n      },\n      \"type\": \"paragraph\"\n    },\n    {\n      \"id\": \"lGOoZV63Rv\",\n      \"data\": {\n        \"text\": \"Nunc nec ultrices odio, vel rutrum tellus. Nulla pharetra nulla eget nisl varius, nec commodo nibh aliquet. Etiam faucibus mauris odio, sit amet bibendum eros placerat a. Mauris pretium mauris eget lacus rutrum, maximus euismod urna bibendum. Nulla fringilla aliquam mi, ut varius est. Curabitur ac mi eget purus elementum malesuada sit amet id justo. Phasellus eget interdum elit, quis pretium velit. Aenean facilisis quam purus, eu semper lacus suscipit ac. In sollicitudin, eros id volutpat molestie, massa elit volutpat risus, a egestas nulla urna in quam. Donec eget massa vestibulum, tempor velit rutrum, tristique est. In tempus mauris eu arcu luctus pulvinar.\"\n      },\n      \"type\": \"paragraph\"\n    },\n    {\n      \"id\": \"KJlhxUxahj\",\n      \"data\": {\n        \"text\": \"Some List\",\n        \"level\": 2\n      },\n      \"type\": \"header\"\n    },\n    {\n      \"id\": \"mvcAAL4qNY\",\n      \"data\": {\n        \"items\": [\n          \"Item 1\",\n          \"Item 2\",\n          \"Item 3\",\n          \"Item 4\"\n        ],\n        \"style\": \"unordered\"\n      },\n      \"type\": \"list\"\n    },\n    {\n      \"id\": \"cliYL8S1N_\",\n      \"data\": {\n        \"text\": \"<b>Ordered List&nbsp;</b>\"\n      },\n      \"type\": \"paragraph\"\n    },\n    {\n      \"id\": \"VJvvaT9ZK2\",\n      \"data\": {\n        \"items\": [\n          \"Item 1\",\n          \"Item 2\",\n          \"Item 3\",\n          \"Item 4\"\n        ],\n        \"style\": \"ordered\"\n      },\n      \"type\": \"list\"\n    }\n  ],\n  \"version\": \"2.30.6\"\n}",
            PublishDate = new DateTime(2024, 9, 15).ToUniversalTime(),
            PostType = PostTypeEnum.Page,
            Status = PostStatus.Published,
            Author = null!,
            AuthorId = authorId,
            Tenant = null!,
            TenantId = tenantId,
        };


        return [post, page];
    }
}