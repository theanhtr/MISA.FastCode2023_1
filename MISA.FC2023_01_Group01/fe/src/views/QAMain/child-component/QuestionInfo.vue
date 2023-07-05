<template>
  <div class="question">
    <div class="flex-row">
      <misa-icon :icon="iconSubject" height="auto" width="auto" />
      <div class="question__info">
        <div class="question__title label-text">
          {{ questionInfo.question_title }}
        </div>
        <div class="question_more-info">
          <div class="owner_name">{{ questionInfo.owner_name }}</div>
          <div class="countRes">1</div>
        </div>
      </div>
    </div>
    <misa-separation-line borderColor="red" />
  </div>
</template>

<script>
export default {
  name: "QuestionInfo",
  props: {
    questionInfo: {
      default: {},
      required: true,
    },
  },
  computed: {
    iconSubject() {
      switch (this.questionInfo.subject_id) {
        case this.$_MISAEnum.SUBJECT.QUESTION:
          return "question--large";

        case this.$_MISAEnum.SUBJECT.DISCUSS:
          return "warning--large";

        case this.$_MISAEnum.SUBJECT.SHARE:
          return "error--large";

        default:
          return "";
      }
    },
  },
};
</script>

<style scoped>
@import url(./question-info.css);
</style>
