<template>
  <div id="main-content">
    <div class="header"></div>
    <div class="page-content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheMainContent",
  methods: {
    /**
     * bắt sự kiện thu gọn sidebar
     * @author: TTANH (25/06/2023)
     */
    collapseSidebar() {
      try {
        this.$store.commit("toggleSidebar");
      } catch (error) {
        console.log(
          "🚀 ~ file: TheMainContent.vue:83 ~ collapseSidebar ~ error:",
          error
        );
      }
    },
  },
};
</script>

<style scoped>
@import url(./header.css);
@import url(./page-content.css);

#main-content {
  height: 100%;
  width: calc(100% - 180px);
  display: flex;
  flex-direction: column;
}
</style>
