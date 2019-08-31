<template>
  <div
    class="alert alert-info"
    v-if="busy"
  >
    <i class="fas fa-spinner fa-spin" />
    {{ message }}
  </div>
</template>

<script>
export default {
  name: 'WaitCursor',
  props: {
    'message': {
      type: String,
      default: ''
    },
    'busy': {
      type: Boolean,
      required: true
    }
  }
}
</script>

<style scoped>
  .fas .fa-spinner {
    color: red;
  }
</style>
