<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Inmuebles</h2>
        <form @submit.prevent="crearInmueble" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.calle" placeholder="Calle" class="input" />
          <input v-model="nuevo.numero" placeholder="Número" class="input" />
          <input v-model="nuevo.orientacion" placeholder="Orientación" class="input" />
          <input v-model="nuevo.referencias" placeholder="Referencias" class="input" />
          <input v-model="nuevo.evaluoFiscal" type="number" placeholder="Evaluo Fiscal" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="i in inmuebles" :key="i.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ i.calle }} {{ i.numero }} - Evaluo: ${{ i.evaluoFiscal }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(i)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarInmueble(i.id)">Eliminar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </template>
  
  <script>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  
  export default {
    setup() {
      const inmuebles = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        calle: '',
        numero: '',
        orientacion: '',
        referencias: '',
        evaluoFiscal: 0,
        estadoId: 1,
        idTipoInmueble: 1,
        idLocalidad: 1
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarInmuebles = async () => {
        const res = await axios.get('/api/admin/gestion/inmuebles', { headers });
        inmuebles.value = res.data;
      };
  
      const crearInmueble = async () => {
        if (editar.value) {
          await axios.put(`/api/inmuebles/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/inmuebles', nuevo.value, { headers });
        }
        await cargarInmuebles();
        limpiarFormulario();
      };
  
      const eliminarInmueble = async (id) => {
        await axios.delete(`/api/admin/gestion/inmuebles/${id}`, { headers });
        await cargarInmuebles();
      };
  
      const seleccionarEditar = (i) => {
        Object.assign(nuevo.value, { ...i });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          calle: '',
          numero: '',
          orientacion: '',
          referencias: '',
          evaluoFiscal: 0,
          estadoId: 1,
          idTipoInmueble: 1,
          idLocalidad: 1
        });
        editar.value = false;
      };
  
      onMounted(() => cargarInmuebles());
  
      return {
        inmuebles,
        nuevo,
        editar,
        crearInmueble,
        eliminarInmueble,
        seleccionarEditar
      };
    }
  };
  </script>
  
  <style scoped>
  .input {
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 0.5rem;
  }
  .btn {
    background: #2d6cdf;
    color: white;
    padding: 0.5rem;
    border-radius: 0.5rem;
    font-weight: bold;
  }
  .card {
    background: white;
    padding: 1rem;
    border-radius: 1rem;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  </style>
  