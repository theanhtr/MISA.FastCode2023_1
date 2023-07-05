const MISAEnum = {
  GENDER: {
    MALE: 0,
    FEMALE: 1,
    ORTHER: 2,
  },

  FORM_MODE: {
    ADD: 0,
    UPDATE: 1,
    VIEW: 2,
  },

  /* enum sử dụng cho sự kiện keypress, keyup, keydown */
  KEY_CODE: {
    TAB: 9,
    ENTER: 13,
    CTRL: 17,
    SHIFT: 16,
    ESC: 27,
    ALT: 18,
    ARROW_UP: 38,
    ARROW_DOWN: 40,
    ARROW_RIGHT: 39,
    ARROW_LEFT: 37,
  },

  /* enum sử dụng trong more info nav của add employee popup */
  MORE_INFO_NAV: {
    SALARY_INFO: 0,
    BANK_INFO: 1,
    CONTACT_INFO: 2,
  },

  SUBJECT: {
    QUESTION: 1,
    DISCUSS: 2,
    SHARE: 3,
  },
};

export default MISAEnum;
