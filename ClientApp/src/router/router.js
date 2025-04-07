import { createRouter, createWebHistory } from 'vue-router';

import Contribuyentes from './AdminEntidad1_Contribuyentes.vue';
import Inmuebles from './AdminEntidad2_Inmuebles.vue';
import Tributos from './AdminEntidad3_Tributos.vue';
import Pagos from './AdminEntidad4_Pagos.vue';
import Notificaciones from './AdminEntidad5_Notificaciones.vue';
import Usuarios from './AdminEntidad6_Usuarios.vue';
import Municipios from './AdminEntidad7_Municipios.vue';
import Estados from './AdminEntidad8_Estados.vue';
import EstadoTributo from './AdminEntidad9_EstadoTributo.vue';
import Localidades from './AdminEntidad10_Localidad.vue';
import TiposDocumento from './AdminEntidad11_TipoDocumento.vue';
import Periodicidades from './AdminEntidad12_Periodicidad.vue';
import TiposImpuesto from './AdminEntidad13_TipoImpuesto.vue';
import ValoresTipoImpuesto from './AdminEntidad14_ValorTipoImpuesto.vue';
import TiposInmueble from './AdminEntidad15_TipoInmueble.vue';
import TitularidadInmueble from './AdminEntidad16_TitularidadInmueble.vue';
import ValuacionesInmueble from './AdminEntidad17_ValuacionInmueble.vue';
import ObrasPublicas from './AdminEntidad18_ObraPublica.vue';
import InmueblesObras from './AdminEntidad19_InmuebleObra.vue';
import MediosPago from './AdminEntidad20_MedioPago.vue';
import PlanesPago from './AdminEntidad21_PlanPago.vue';
import CuotasPlan from './AdminEntidad22_CuotaPlan.vue';
import InteresesMora from './AdminEntidad23_InteresMora.vue';
import PlantillasNotificacion from './AdminEntidad24_PlantillaNotificacion.vue';
import ColaNotificacion from './AdminEntidad25_ColaNotificacion.vue';
import Auditoria from './AdminEntidad26_Auditoria.vue';

const routes = [
  { path: '/admin/contribuyentes', component: Contribuyentes },
  { path: '/admin/inmuebles', component: Inmuebles },
  { path: '/admin/tributos', component: Tributos },
  { path: '/admin/pagos', component: Pagos },
  { path: '/admin/notificaciones', component: Notificaciones },
  { path: '/admin/usuarios', component: Usuarios },
  { path: '/admin/municipios', component: Municipios },
  { path: '/admin/estados', component: Estados },
  { path: '/admin/estadostributo', component: EstadoTributo },
  { path: '/admin/localidades', component: Localidades },
  { path: '/admin/tiposdocumento', component: TiposDocumento },
  { path: '/admin/periodicidades', component: Periodicidades },
  { path: '/admin/tiposimpuesto', component: TiposImpuesto },
  { path: '/admin/valorestipoimpuesto', component: ValoresTipoImpuesto },
  { path: '/admin/tiposinmueble', component: TiposInmueble },
  { path: '/admin/titularidadinmueble', component: TitularidadInmueble },
  { path: '/admin/valuacionesinmueble', component: ValuacionesInmueble },
  { path: '/admin/obraspublicas', component: ObrasPublicas },
  { path: '/admin/inmueblesobras', component: InmueblesObras },
  { path: '/admin/mediospago', component: MediosPago },
  { path: '/admin/planespago', component: PlanesPago },
  { path: '/admin/cuotasplan', component: CuotasPlan },
  { path: '/admin/interesmora', component: InteresesMora },
  { path: '/admin/plantillasnotificacion', component: PlantillasNotificacion },
  { path: '/admin/colanotificacion', component: ColaNotificacion },
  { path: '/admin/auditoria', component: Auditoria }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
