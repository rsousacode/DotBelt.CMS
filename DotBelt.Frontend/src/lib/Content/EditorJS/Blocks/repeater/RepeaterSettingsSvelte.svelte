<script lang="ts">
  import type {RepeaterSettingsProps} from "$lib/Content/EditorJS/Blocks/repeater/RepeaterSettingsProps";

  let {numberOfPosts = $bindable(), onNumberOfPostsChanged} : RepeaterSettingsProps = $props();

</script>

<div class="repeater-settings">
  <label class="number-posts-label">Count</label>
  <input class="number-posts-input" type="number" onchange={() => numberOfPosts ? onNumberOfPostsChanged(numberOfPosts) : {}} bind:value={numberOfPosts} />
</div>


<style>
  .number-posts-input {
    max-width: 40px;
    margin-right: 8px;
  }
  .number-posts-label {
    font-size: 14px;
    margin-right: 10px;
    margin-left: 15px;
  }

  .repeater-settings {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
</style>