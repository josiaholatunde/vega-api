import { TestBed } from '@angular/core/testing';

import { MakesService } from './makes.service';

describe('MakesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MakesService = TestBed.get(MakesService);
    expect(service).toBeTruthy();
  });
});
