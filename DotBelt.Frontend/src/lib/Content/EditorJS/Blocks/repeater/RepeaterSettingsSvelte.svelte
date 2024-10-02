<script lang="ts">
  import type {RepeaterSettingsProps} from "$lib/Content/EditorJS/Blocks/repeater/RepeaterSettingsProps";

  let {numberOfPosts = $bindable(), onNumberOfPostsChanged} : RepeaterSettingsProps = $props();

</script>

<div class="repeater-settings">
  <label class="number-posts-label">Number of posts</label>
  <input class="number-posts-input" type="number" onchange={() => onNumberOfPostsChanged(numberOfPosts)} bind:value={numberOfPosts} />
</div>


<style>
  .number-posts-input {
    max-width: 50px;
  }
  .number-posts-label {
    font-size: 14px;
    margin-right: 10px;
  }
  .repeater-settings {
    display: flex;
    justify-content: center;
    align-items: center;
  }
</style>