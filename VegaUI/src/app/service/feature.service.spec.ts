import { TestBed } from '@angular/core/testing';

import { FeaturesService } from './feature.service';

describe('FeatureService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FeaturesService = TestBed.get(FeaturesService);
    expect(service).toBeTruthy();
  });
});
