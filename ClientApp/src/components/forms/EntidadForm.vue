<template>
  <form @submit.prevent="onSubmit" class="grid grid-cols-2 gap-4 mt-4">
    <slot name="fields"></slot>
    <button type="submit" class="btn col-span-2">{{ editing ? 'Actualizar' : 'Crear' }}</button>
  </form>
</template>

<script setup>
const props = defineProps({
  editing: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits(['submit']);

const onSubmit = () => {
  emit('submit');
};
</script>