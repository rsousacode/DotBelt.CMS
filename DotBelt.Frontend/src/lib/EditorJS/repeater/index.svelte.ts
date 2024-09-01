import RepeaterSvelte from "$lib/EditorJS/repeater/RepeaterSvelte.svelte";
import {mount, unmount} from "svelte";
import type {API, BlockTune} from "@editorjs/editorjs";
import {type GraphQlQuery, type Maybe, type Post, type PostsConnection, PostTypeEnum} from "$lib/GraphQL/generated";
import {gql} from "@apollo/client/core";

type RepeaterData = { hasImage: boolean, posts: Maybe<Post[]> | undefined }

type ConstructorArgs = { data: RepeaterData, api: API };

export default class Repeater {

    svelteComponent: { $set?: any; $on?: any; } | undefined;
    data: RepeaterData = $state({hasImage: false, posts: []})

    posts: Maybe<Post[]> | undefined;
    api: API | undefined;

    targetElement: Element;

    constructor({data, api}: ConstructorArgs) {
        this.targetElement = document.createElement('div');
        this.data = data;
    }


    async getPosts(type: PostTypeEnum): Promise<Maybe<PostsConnection | undefined>> {

        const query = gql`
            query GetPosts($type: PostTypeEnum!, $first: Int) {
                posts(first: $first, order: { publishDate: DESC }, where: { postType: { eq: $type } }) {
                    nodes {
                        id
                        title
                        description
                    }
                }
            }
        `

        // @ts-ignore
        const client = globalThis.GetApolloSvelteClient();
        // @ts-ignore
        const {data: {posts}, errors} = await client.query<GraphQlQuery>({
            query: query,
            variables: {type: type}
        });

        return posts;
    }

    renderSettings(): BlockTune[] {
        return [
            {
                icon: "",
                label: "With image",
                toggle: true,
                isActive: this.data?.hasImage,
                onActivate: () => {
                    this.data.hasImage = !this.data.hasImage;
                },
                closeOnActivate: false,
                render: () => document.createElement('div')
            }
        ]
    }

    static get toolbox() {
        return {
            title: 'Repeater',
            icon: '<svg width="17" height="15" viewBox="0 0 336 276" xmlns="http://www.w3.org/2000/svg"><path d="M291 150V79c0-19-15-34-34-34H79c-19 0-34 15-34 34v42l67-44 81 72 56-29 42 30zm0 52l-43-30-56 30-81-67-66 39v23c0 19 15 34 34 34h178c17 0 31-13 34-29zM79 0h178c44 0 79 35 79 79v118c0 44-35 79-79 79H79c-44 0-79-35-79-79V79C0 35 35 0 79 0z"/></svg>'
        };
    }

    render() {

        this.getPosts(PostTypeEnum.Post).then(data => {
            if (data) {
                this.data.posts = data.nodes;
            }
            this.svelteComponent = mount(RepeaterSvelte, {target: this.targetElement, props: this.data});

        })

        return this.targetElement;

    }

    save() {
        return {
            hasImage: this.data.hasImage
        }
    }
}