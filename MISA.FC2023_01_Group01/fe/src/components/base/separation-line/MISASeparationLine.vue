<template>
  <div class="separation-line" :style="{ borderColor: borderColor }"></div>
</template>

<script>
export default {
  name: "MISASeparationLine",
  props: {
    borderColor: "",
  },
};
</script>

<style scoped>
@import url(./separation-line.css);
</style>
