<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Tipos de Impuesto</h2>
        <form @submit.prevent="crearTipoImpuesto" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.descripcion" placeholder="Descripción" class="input" />
          <input v-model="nuevo.resolucionAsociada" placeholder="Resolución Asociada" class="input" />
          <input v-model="nuevo.valorFijo" type="number" step="0.01" placeholder="Valor Fijo" class="input" />
          <select v-model="nuevo.tieneTablaValores" class="input">
            <option :value="true">Con tabla de valores</option>
            <option :value="false">Sin tabla de valores</option>
          </select>
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="t in tiposImpuesto" :key="t.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ t.descripcion }} - ${{ t.valorFijo }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(t)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarTipoImpuesto(t.id)">Eliminar</button>
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
      const tiposImpuesto = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        descripcion: '',
        resolucionAsociada: '',
        valorFijo: 0.00,
        tieneTablaValores: false,
        idMunicipio: 1,
        idPeriodicidad: 1
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarTiposImpuesto = async () => {
        const res = await axios.get('/api/admin/catalogos/tipos-impuesto', { headers });
        tiposImpuesto.value = res.data;
      };
  
      const crearTipoImpuesto = async () => {
        if (editar.value) {
          await axios.put(`/api/tiposimpuesto/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/catalogos/tipos-impuesto', nuevo.value, { headers });
        }
        await cargarTiposImpuesto();
        limpiarFormulario();
      };
  
      const eliminarTipoImpuesto = async (id) => {
        await axios.delete(`/api/admin/catalogos/tipos-impuesto/${id}`, { headers });
        await cargarTiposImpuesto();
      };
  
      const seleccionarEditar = (t) => {
        Object.assign(nuevo.value, { ...t });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          descripcion: '',
          resolucionAsociada: '',
          valorFijo: 0.00,
          tieneTablaValores: false,
          idMunicipio: 1,
          idPeriodicidad: 1
        });
        editar.value = false;
      };
  
      onMounted(() => cargarTiposImpuesto());
  
      return {
        tiposImpuesto,
        nuevo,
        editar,
        crearTipoImpuesto,
        eliminarTipoImpuesto,
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
  