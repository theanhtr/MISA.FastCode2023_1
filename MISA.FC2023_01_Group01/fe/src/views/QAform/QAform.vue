<template>
  <div class="Qa-form-wrap">
    <div class="Qa-header">
      <div class="Qa-title">Tiêu đề {{ questionName }}</div>
      <div class="Qa-content-wrap">
        <div class="Qa-author">
          <div class="name-author">
            <p>Người đăng:</p>
            {{ nameAuthor }}
          </div>
          <div class="Qa-content">{{ QaContent }}</div>
        </div>
      </div>
    </div>
    <div class="Qa-main">
      <div v-if="isShowAnswer" class="answer-main">
        <div class="name-author-answer">
          <misa-textfield v-model="authorAnswer"></misa-textfield>
        </div>
        <div class="answer">
          <misa-textfield v-model="answerContent"></misa-textfield>
          <span class="error-mess"></span>
        </div>
      </div>
      <div class="Qa-action">
        <misa-button v-if="isShowAnswer" @click="onCloseAnswer" type="main">
          Hủy</misa-button
        >
        <misa-button type="main" @click="onShowAnswer">Trả lời</misa-button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Qa-form",
  data() {
    return {
      isShowAnswer: false,
      answerContent: "",
      authorAnswer: "",
    };
  },
  methods: {
    /**
     * click vào sẽ hiện thị bảng trả lời
     */
    onShowAnswer() {
      this.isShowAnswer = true;
    },
    onCloseAnswer() {
      this.isShowAnswer = false;
    },
  },
  props: {
    /**Tiêu đề câu hỏi */
    questionName: {
      type: String,
      default: "",
    },
    /**Tên người đăng câu hỏi */
    nameAuthor: {
      type: String,
      default: "",
    },
    /**Nội dung câu hỏi */
    QaContent: {
      type: String,
      default: "",
    },
  },
};
</script>

<style>
@import url(./Qa-form.css);
</style>
