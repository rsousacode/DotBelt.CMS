import RepeaterSvelte from './RepeaterSvelte.svelte';
import {mount, unmount} from 'svelte';
import type { API, BlockTune } from '@editorjs/editorjs';
import {
  type DotBeltQuery,
  type Maybe,
  type PostResponse,
  type PostsConnection,
  PostTypeEnum,
} from '$lib/API/GraphQL/generated';
import { gql } from '@apollo/client/core';

import {apolloClientStore} from "$lib/API/GraphQL/apolloClientStore";
import RepeaterSettingsSvelte from "$lib/Content/EditorJS/Blocks/repeater/RepeaterSettingsSvelte.svelte";
import type {RepeaterSettingsProps} from "$lib/Content/EditorJS/Blocks/repeater/RepeaterSettingsProps";

type RepeaterData = {
  hasImage: boolean
  numberOfPosts: number
  variables: Record<string, any>
};


type ConstructorArgs = { data: RepeaterData; api: API };
type RenderingComponentProps = {posts: PostResponse[], hasImage: boolean}

export default class Repeater {
  svelteComponent: { $set?: any; $on?: any } | undefined;

  data: RepeaterData = $state(
    {
      hasImage: true,
      numberOfPosts: 3,
      variables: {first: 5, type: PostTypeEnum.Post}
    }
  );

  query: string = `
   query GetPosts($type: PostTypeEnum!, $first: Int) {
        posts(first: $first, order: { publishDate: DESC }, where: { postType: { eq: $type } }) {
          nodes {
            id
            title
            description,
            featuredImage {
              relativeUrl
            }
          }
        }
      }
  `;

  renderingComponentProps : RenderingComponentProps = $state({posts: [], hasImage: this.data.hasImage});

  posts: Maybe<PostResponse[]> | undefined = $state();
  api: API | undefined;

  targetElement: Element;

  constructor({ data, api }: ConstructorArgs) {
    this.targetElement = document.createElement('div');
    if(data.variables === undefined) {
      this.data =  {
        hasImage: true,
        numberOfPosts: 3,
        variables: {first: 5, type: PostTypeEnum.Post}
      };
    } else {
      this.data = data;
    }
  }

   getPosts : () => Promise<Maybe<PostsConnection | undefined>> =  async () => {

    const client = apolloClientStore.getClient();

    const {
      data: { posts }
    } = await client.query<DotBeltQuery>({
      query: gql(this.query),
      variables: this.data.variables,
    });

    return posts;
  }


  // noinspection JSUnusedGlobalSymbols
  renderSettings(): HTMLElement {

    const wrapper = document.createElement('div');

    type RepeaterSettingsData = {
      props: RepeaterSettingsProps,
      target: HTMLElement
    }

    const repeaterData : RepeaterSettingsData = {
      target: wrapper,
      props: {
        numberOfPosts: this.data.variables?.first as number,
        onNumberOfPostsChanged: (newNumber) => {
          this.data.variables.first = newNumber;
          this.refetchPosts();
        }
      }
    };

    // @ts-ignore
    mount(RepeaterSettingsSvelte, repeaterData);

    return wrapper;
  }

  setPosts(posts: PostResponse[]) {
    this.renderingComponentProps.posts = posts;
  }

  // noinspection JSUnusedGlobalSymbols
  static get toolbox() {
    return {
      title: 'Repeater',
      icon: '<svg width="17" height="15" viewBox="0 0 336 276" xmlns="http://www.w3.org/2000/svg"><path d="M291 150V79c0-19-15-34-34-34H79c-19 0-34 15-34 34v42l67-44 81 72 56-29 42 30zm0 52l-43-30-56 30-81-67-66 39v23c0 19 15 34 34 34h178c17 0 31-13 34-29zM79 0h178c44 0 79 35 79 79v118c0 44-35 79-79 79H79c-44 0-79-35-79-79V79C0 35 35 0 79 0z"/></svg>',
    };
  }

  refetchPosts = () => {
    this.getPosts().then((data) => {
      if (data && data.nodes) {
        this.setPosts(data.nodes);
      }
    });
  }

  // noinspection JSUnusedGlobalSymbols
  render() {
    if(this.svelteComponent) {
      unmount(this.svelteComponent);
    }

    this.getPosts().then((data) => {
      if (data && data.nodes) {
        this.setPosts(data.nodes);
      }
      this.svelteComponent = mount(RepeaterSvelte, {
        target: this.targetElement,
        props: this.renderingComponentProps,
      });
    });
    return this.targetElement;
  }

  // noinspection JSUnusedGlobalSymbols
  save() {
    return {
      hasImage: this.data.hasImage,
      query: this.query,
      variables: this.data.variables,
    };
  }
}
