<script lang="ts">
  import {onMount} from "svelte";

  function onDragEnter(e: DragEvent) {
    e.preventDefault();
    console.log('drag enter')
    //dropZone.classList.add(hoverClassName);
  }

  function onDragover(e: DragEvent) {
    e.preventDefault();
    //dropZone.classList.add(hoverClassName);
  }

  function onDragleave(e: DragEvent) {
    e.preventDefault();
    //dropZone.classList.remove(hoverClassName);
  }

  function onDrop(e: DragEvent) {
    e.preventDefault();
    //dropZone.classList.remove(hoverClassName);

    if(e.dataTransfer === null) return;

    const files = Array.from(e.dataTransfer.files);

    if (files.length > 0) {
      const data = new FormData();
      for (const file of files) {
        data.append('files', file);
      }



      fetch('/api/uploads', {
        method: 'POST',
        body: data

      })
        .then(async (data) => {
          const response = await data.json();
          console.log(response);
        })
        .catch(reason => console.error(reason));
    }
  }


</script>



<svelte:body  ondragover={onDragover}
              ondrop={onDrop}
              ondragenter={onDragEnter}
              ondragleave={onDragleave}
/>

