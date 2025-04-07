<template>
  <div class="fixed inset-0 flex items-center justify-center z-50">
    <div class="absolute inset-0 bg-black opacity-50" @click="onCancel"></div>
    <div class="bg-white rounded-lg p-6 w-96 z-10">
      <h3 class="text-xl font-bold mb-4">{{ title }}</h3>
      <p class="mb-6">{{ message }}</p>
      <div class="flex justify-end space-x-2">
        <button 
          @click="onCancel" 
          class="px-4 py-2 border rounded-md hover:bg-gray-100"
        >
          Cancelar
        </button>
        <button 
          @click="onConfirm" 
          class="px-4 py-2 bg-red-500 text-white rounded-md hover:bg-red-600"
        >
          Confirmar
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  title: {
    type: String,
    required: true
  },
  message: {
    type: String,
    required: true
  }
});

const emit = defineEmits(['confirm', 'cancel']);

const onConfirm = () => {
  emit('confirm');
};

const onCancel = () => {
  emit('cancel');
};
</script>