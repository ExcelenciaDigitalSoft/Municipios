<template>
  <div class="card mt-4">
    <div class="flex justify-between items-center p-2 border-b">
      <h3 class="font-medium">{{ title }}</h3>
      <slot name="actions"></slot>
    </div>
    
    <div v-if="loading" class="p-4 text-center">
      <span class="spinner"></span> Cargando...
    </div>
    
    <div v-else-if="!items.length" class="p-4 text-center text-gray-500">
      No hay registros para mostrar
    </div>
    
    <ul v-else>
      <li v-for="item in items" :key="getId(item)" class="py-2 border-b flex justify-between items-center">
        <slot name="item" :item="item"></slot>
      </li>
    </ul>
    
    <div v-if="paginated" class="p-2 flex justify-center">
      <!-- Controles de paginación -->
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  items: {
    type: Array,
    required: true
  },
  loading: {
    type: Boolean,
    default: false
  },
  title: {
    type: String,
    default: 'Registros'
  },
  paginated: {
    type: Boolean,
    default: false
  },
  idField: {
    type: String,
    default: 'id'
  }
});

const getId = (item) => item[props.idField];
</script>